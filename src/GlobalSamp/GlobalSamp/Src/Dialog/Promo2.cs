using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

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
                PlayerManager.Instance.AddPlayer(data);
                Response -= OnPromo2ChangedResponse;
                return;
            }
            PlayerManager.Instance.AddPlayer(data);
            Response -= OnPromo2ChangedResponse;
        }
    }
}