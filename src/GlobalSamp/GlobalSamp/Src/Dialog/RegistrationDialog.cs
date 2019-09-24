using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class RegistrationDialog : InputDialog
    {
        public RegistrationDialog(bool playerRegistered)
            : base(playerRegistered ? Translator.Instance.GetMessage("enterDialogCaption") : Translator.Instance.GetMessage("registerDialogCaption"),
                playerRegistered
                    ? Translator.Instance.GetMessage("enterDialog")
                    : Translator.Instance.GetMessage("registerDialog"),
                true,
                Translator.Instance.GetMessage("leftButton"))
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
                e.Player.SendClientMessage(Translator.Instance.GetMessage("not_password"));
                e.Player.Kick();
                return;
            }

            PlayerData data = PlayerManager.Instance.GetPlayerData(e.Player.Name);
            if (data != null)
            {
                if (data.Password == e.InputText)
                {
                    data.Authorized = true;
                    e.Player.ForceClassSelection();
                    e.Player.SetSpawnInfo(1, 181, SpawnPosition.DEFAULT, 1);
                    e.Player.Spawn();
                }
                else
                {
                    Response -= OnInputRegistrationData;
                    int i;
                    for (i = 3; i != 0; i--)
                    {
                        if (data.Password == e.InputText)
                        {
                            data.Authorized = true;
                            e.Player.VirtualWorld = -1;
                            e.Player.ToggleSpectating(false);
                            e.Player.ForceClassSelection();
                            e.Player.SetSpawnInfo(1, 181, SpawnPosition.DEFAULT, 1);
                            e.Player.Spawn();
                        }
                        else
                        {
                            InputDialog dialog = new InputDialog(
                                Translator.Instance.GetMessage("wrongPass_caption"),
                                Translator.Instance.GetMessage("wrongPass_message") + i,
                                true,
                                Translator.Instance.GetMessage("leftButton"));
                            dialog.Show(e.Player);
                        }
                    }
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