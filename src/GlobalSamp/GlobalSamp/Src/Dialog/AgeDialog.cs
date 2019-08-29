using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace GlobalSamp.Dialog
{
    public class AgeDialog : InputDialog
    {
        public AgeDialog() : base(
            "Ваш возраст",
            "Введите Ваш возраст",
            false,
            "Далее")
        {
        }
        public void OnAgeDialogResponse(object sender, DialogResponseEventArgs e)
        {
            var promo1 = new Promo1();
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                Response -= OnAgeDialogResponse;
                // TODO: Logging
                return;
            }
            if (e.DialogButton != DialogButton.Left)
            {
                data.SkinColor = PlayerColor.BLACK;
                Response -= OnAgeDialogResponse;
                promo1.Response += promo1.OnPromo1Response;
                promo1.Show(e.Player);
                return;
            }
            Response -= OnAgeDialogResponse;
            promo1.Response += promo1.OnPromo1Response;
            promo1.Show(e.Player);
        }
    }
}