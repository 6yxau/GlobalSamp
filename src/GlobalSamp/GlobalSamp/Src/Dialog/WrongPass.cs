using GlobalSamp.Application.Translator;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class WrongPass : InputDialog
    {
        public WrongPass() : base(
            Translator.Instance.GetMessage("wrongPass_caption"),
            Translator.Instance.GetMessage("wrongPass_message" + 3),
            true,
            Translator.Instance.GetMessage("leftButton"))
        {
        }

        public void OnWrongPassDialogResponse(object sender, DialogResponseEventArgs e)
        {
            if (e.DialogButton != DialogButton.Left)
            {
                e.Player.SendClientMessage(Translator.Instance.GetMessage("not_password"));
                e.Player.Kick();
                return;
            }

            int i = 3;
            
            PlayerData data = PlayerManager.Instance.GetPlayerData(e.Player.Name);
            while (i != 0)
            {
                if (data.Password == e.InputText)
                {
                    data.Authorized = true;
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
                i--;
            }

            if (i == 0)
            {
                e.Player.SendClientMessage(Translator.Instance.GetMessage("wrongPass_end"));
                e.Player.Kick();
            }
            if (data.Password == e.InputText)
            {
                data.Authorized = true;
                e.Player.ToggleSpectating(false);
                e.Player.ForceClassSelection();
                e.Player.SetSpawnInfo(1, 181, SpawnPosition.DEFAULT, 1);
                e.Player.Spawn();
            }
            else
            {
                RegistrationDialog dialog = new RegistrationDialog(true);
                dialog.Response += dialog.OnInputRegistrationData;
                dialog.Show(e.Player);
            }
        }
    }
}