using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace GlobalSamp.Dialog
{
    public class SkinColorDialog : MessageDialog
    {
        public SkinColorDialog() : base
            ("Цвет кожи",
            "Выберите ваш цвет кожи",
            "Светлый",
            "Тёмный")
        {
        }
        public void OnSkinColorDialogResponse(object sender, DialogResponseEventArgs e)
        {
            var nationalitySet = new NationalityList();
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                Response -= OnSkinColorDialogResponse;
                // TODO: Logging
                return;
            }
            if (e.DialogButton != DialogButton.Left)
            {
                data.SkinColor = PlayerColor.BLACK;
                Response -= OnSkinColorDialogResponse;
                nationalitySet.Response += nationalitySet.OnNationalitySetResponse;
                nationalitySet.Show(e.Player);
                return;
            }
            Response -= OnSkinColorDialogResponse;
            nationalitySet.Response += nationalitySet.OnNationalitySetResponse;
            nationalitySet.Show(e.Player);
        }
    }
}