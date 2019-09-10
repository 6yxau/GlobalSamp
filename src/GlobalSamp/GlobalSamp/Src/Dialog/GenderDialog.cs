using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class GenderDialog : MessageDialog
    {
        public GenderDialog() : base(
            Translator.Instance.GetMessage("genderDialogCaption"),
            Translator.Instance.GetMessage("genderDialog"),
            Translator.Instance.GetMessage("genderDialogLeft"),
            Translator.Instance.GetMessage("genderDialogRight"))
        {
        }

        public void OnGenderDialogResponse (object sender, DialogResponseEventArgs e)
        {
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            var skinColorDialog = new SkinColorDialog();
            if (data == null)
            {
                Response -= OnGenderDialogResponse;
                e.Player.Kick();
                // TODO: Logging
                return;
            }
            if (e.DialogButton != DialogButton.Left)
            {
                data.Gender = PlayerGender.FEMALE;
                skinColorDialog.Response += skinColorDialog.OnSkinColorDialogResponse;
                skinColorDialog.ShowAsync(e.Player);
                Response -= OnGenderDialogResponse;
                return;
            }
            Response -= OnGenderDialogResponse;
            skinColorDialog.Response += skinColorDialog.OnSkinColorDialogResponse;
            skinColorDialog.ShowAsync(e.Player);
        }
    }
}