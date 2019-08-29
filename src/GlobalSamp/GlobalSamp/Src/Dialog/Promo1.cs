using System;
using GlobalSamp.Player;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;

namespace GlobalSamp.Dialog
{
    public class Promo1 : ListDialog
    {
        public Promo1() : base(
            "Откуда вы узнали о GlobalRP?",
            "Далее")
        {
            AddItem("От друзей");
            AddItem("На YouTUBE");
            AddItem("Вкладка Hosted");
            AddItem("На порталах/форумах");
            AddItem("В поисковике");
            AddItem("Другое");
        }

        public void OnPromo1Response(object sender, DialogResponseEventArgs e)
        {
            Console.WriteLine(e.InputText);
            var friend = new FrienInGame();
            PlayerData data = PlayerManager.Instance.GetCachedPlayerData(e.Player.Name);
            if (data == null)
            {
                Response -= OnPromo1Response;
                // TODO: Logging
                return;
            }
            if (e.DialogButton != DialogButton.Left)
            {
                Response -= OnPromo1Response;
                friend.Response += friend.OnFrienInGameChangedResponse;
                friend.Show(e.Player);
                return;
            }
            Response -= OnPromo1Response;
            friend.Response += friend.OnFrienInGameChangedResponse;
            friend.Show(e.Player);
        }
    }
}