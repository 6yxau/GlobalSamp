using System.Collections;
using System.Collections.Generic;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class NationalityList : ListDialog
    {
        public NationalityList() : base(
            "Выберите Вашу национальность",
            "Далее")
        {
            AddItem("Албанец");
            AddItem("Американец");
            AddItem("Англичанин");
            AddItem("Израилетянин");
            AddItem("Испанец");
            AddItem("Итальянец");
            AddItem("Латиноамериканец");
            AddItem("Мексиканец");
            AddItem("Немец");
            AddItem("Потругалец");
            AddItem("Русский");
            AddItem("Серб");
            AddItem("Скандинав");
            AddItem("Француз");
            AddItem("Японец");
        }

        public void OnNationalitySetResponse(object sender, DialogResponseEventArgs e)
        {
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            var ageDialog = new AgeDialog();
            if (data == null)
            {
                Response -= OnNationalitySetResponse;
                e.Player.Kick();
                // TODO: Logging
                return;
            }
            if (e.DialogButton != DialogButton.Left)
            {
                ageDialog.Response += ageDialog.OnAgeDialogResponse;
                ageDialog.ShowAsync(e.Player);
                Response -= OnNationalitySetResponse;
                return;
            }
            Response -= OnNationalitySetResponse;
            ageDialog.Response += ageDialog.OnAgeDialogResponse;
            ageDialog.Show(e.Player);
        }
    }
}