using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class AgeDialog : InputDialog
    {
        public AgeDialog() : base(
            Translator.Instance.GetMessage("ageDialogCaption"),
            Translator.Instance.GetMessage("ageDialog"),
            false,
            Translator.Instance.GetMessage("leftButton"))
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
                Response -= OnAgeDialogResponse;
                promo1.Response += promo1.OnPromo1Response;
                promo1.Show(e.Player);
                return;
            }
            Response -= OnAgeDialogResponse;
            if (int.TryParse(e.InputText, out int age))
            {
                data.Age = age;
            }
            else
            {
                Response -= OnAgeDialogResponse;
            }
            promo1.Response += promo1.OnPromo1Response;
            promo1.Show(e.Player);
        }
    }
}