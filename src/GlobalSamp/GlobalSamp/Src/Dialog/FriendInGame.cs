using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class FriendInGame : InputDialog
    {
        public FriendInGame() : base(
            Translator.Instance.GetMessage("friendInGameCaption"),
            Translator.Instance.GetMessage("friendInGame"),
            false,
            Translator.Instance.GetMessage("leftButtonCaption"),
            Translator.Instance.GetMessage("rightButton"))
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