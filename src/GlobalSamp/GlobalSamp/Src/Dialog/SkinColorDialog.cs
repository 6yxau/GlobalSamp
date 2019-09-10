using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class SkinColorDialog : MessageDialog
    {
        public SkinColorDialog() : base
            (Translator.Instance.GetMessage("skinColorCaption"),
            Translator.Instance.GetMessage("skinColor"),
            Translator.Instance.GetMessage("skinColorLeft"),
            Translator.Instance.GetMessage("skinColorRight"))
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