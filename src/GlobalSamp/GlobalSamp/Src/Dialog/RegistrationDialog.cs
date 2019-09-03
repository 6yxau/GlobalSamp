using System;
using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace GlobalSamp.Dialog
{
    public class RegistrationDialog : InputDialog
    {
        public RegistrationDialog(bool playerRegistered)
            : base(playerRegistered ? Translator.Instance.GetMessage("enterDialogCaption") : Translator.Instance.GetMessage("registerDialogCaption"),
                playerRegistered
                    ? Translator.Instance.GetMessage("enterDialog")
                    : Translator.Instance.GetMessage("registerDialog"),
                true, "Далее")
        {
        }

        public void OnInputRegistrationData(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton != DialogButton.Left)
            {
                Response -= OnInputRegistrationData;
                if (e.Player == null)
                {
                    return;
                }

                e.Player.SendClientMessage("Вы не ввели пароль");
                e.Player.Kick();
                return;
            }

            PlayerData data = PlayerManager.Instance.GetPlayerData(e.Player.Name);
            if (data != null)
            {
                if (data.Password == e.InputText)
                {
                    data.Authorized = true;
                }
            }
            else
            {
                
                Response -= OnInputRegistrationData;
                data = new PlayerData
                {
                    UserName = e.Player.Name,
                    Password = e.InputText
                };
                PlayerManager.Instance.CachePlayerData(data);
                //TODO: проверить пароль на запрещенные символы
                var passRequestDialog = new PasswordRequestDialog();
                passRequestDialog.Response += passRequestDialog.OnPasswordRequestResponse;
                passRequestDialog.ShowAsync(e.Player);
            }
        }
    }
}