using System;
using System.Linq;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace GlobalSamp.Dialog
{
    public class PasswordRequestDialog : InputDialog
    {
        public PasswordRequestDialog(bool retrial = false) : base(
            "Повтор пароля",
            retrial ? "Вы неверно ввели пароль.\n Введите пароль еще раз" : "Введите пароль еще раз",
            true, 
            "Далее")
        {
        }

        public void OnPasswordRequestResponse(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton != DialogButton.Left)
            {
                Response -= OnPasswordRequestResponse;
                e.Player.Kick();
                return;
            }
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                Response -= OnPasswordRequestResponse;
                e.Player.Kick();
                // TODO: Logging
                return;
            }

            if (e.InputText != data.Password)
            {
                Response -= OnPasswordRequestResponse;
                var retrialDialog = new PasswordRequestDialog(true);
                retrialDialog.Response += retrialDialog.OnPasswordRequestResponse;
                retrialDialog.ShowAsync(e.Player);
                return;
            }
            Response -= OnPasswordRequestResponse;
            var emailDialog = new EmailRegistrationDialog();
            emailDialog.Response += emailDialog.OnEmailRegistrationDialogResponse;
            emailDialog.ShowAsync(e.Player);
        }
    }
}