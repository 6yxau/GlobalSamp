using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class FrienInGame : InputDialog
    {
        public FrienInGame() : base(
            "Введите ник друга, который здесь играет",
            "{18ff00} После достижения Вами 5 уровня он получит вознаграждение",
            false,
            "Ввести",
            "Пропустить")
        {
        }

        public void OnFrienInGameChangedResponse(object sender, DialogResponseEventArgs e)
        {
            var promo2 = new Promo2();
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                Response -= OnFrienInGameChangedResponse;
                // TODO: Logging
                return;
            }
            if (e.DialogButton != DialogButton.Left)
            {
                Response -= OnFrienInGameChangedResponse;
                promo2.Response += promo2.OnPromo2ChangedResponse;
                promo2.Show(e.Player);
                return;
            }
            Response -= OnFrienInGameChangedResponse;
            promo2.Response += promo2.OnPromo2ChangedResponse;
            promo2.Show(e.Player);
        }
    }
}