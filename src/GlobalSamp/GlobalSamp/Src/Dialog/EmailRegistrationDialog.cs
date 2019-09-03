using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace GlobalSamp.Dialog
{
    public class EmailRegistrationDialog : InputDialog
    {
        public EmailRegistrationDialog() : base(
            Translator.Instance.GetMessage("email"), 
            "Введите адрес Вашей электронной почты. \n" +
                      "{18ff00} С помощью почты вы можете восстановить доступ \n" +
                      "{18ff00} к вашему аккаунту в случае взлома или потери пароля.\n" +
                      "{000000}На вашу почту мы отправим письмо со ссылкой. \n" +
                      "{000000}Перейдите по ней для подтверждения регистрации.",
            false,
            "Далее")
        {
        }

        public void OnEmailRegistrationDialogResponse(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton != DialogButton.Left)
            {
                Response -= OnEmailRegistrationDialogResponse;
                e.Player.Kick();
                return;
            }

            if (string.IsNullOrEmpty(e.InputText) || e.InputText.Split("@").Length <= 1)
            {
                //TODO: показывать диалог еще раз   
            }
            
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                Response -= OnEmailRegistrationDialogResponse;
                e.Player.Kick();
                // TODO: Logging
                return;
            }
            Response -= OnEmailRegistrationDialogResponse;
            data.Email = e.InputText;
            var gender = new GenderDialog();
            gender.Response += gender.OnGenderDialogResponse;
            gender.ShowAsync(e.Player);
        }
    }
}