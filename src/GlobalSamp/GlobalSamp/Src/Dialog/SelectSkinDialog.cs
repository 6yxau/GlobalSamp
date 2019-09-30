using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class SelectSkinDialog : InputDialog
    {
        public SelectSkinDialog() : base("Skin id", "Укажиите id скина", false, "Выбрать", "Сохранить")
        {
        }

        public void OnSelectSkinResponse(object sender, DialogResponseEventArgs e)
        {
            e.Player.Skin = int.Parse(e.InputText);
            if (e.DialogButton == DialogButton.Left)
            {
                Response -= OnSelectSkinResponse;
                
                SelectSkinDialog dialog = new SelectSkinDialog();
                dialog.Response += dialog.OnSelectSkinResponse;
                dialog.Show(e.Player);
                return;
            }

            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                e.Player.Kick();
            }
            PlayerManager.Instance.AddPlayer(data);
            PlayerManager.Instance.RemovePlayerDataFromCache(e.Player.Name);
            
            e.Player.SetSpawnInfo(1, e.Player.Skin, SpawnPosition.DEFAULT, 0);
            e.Player.ToggleControllable(true);
            e.Player.VirtualWorld = -1;
            e.Player.Spawn();
            Response -= OnSelectSkinResponse;
        }
    }
}