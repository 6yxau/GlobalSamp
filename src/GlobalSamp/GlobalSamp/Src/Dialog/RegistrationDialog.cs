using System;
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
            : base(playerRegistered ? "Вход" : "Регистрация",
                playerRegistered
                    ? "Ваш логин: {18ff00}зарегистрирован на сервере.\n {ffffff} Для входа введите пароль."
                    : "Ваш логин: {ff0000}не зарегистрирован сервере.\n {ffffff} Для регистрации введите пароль.",
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