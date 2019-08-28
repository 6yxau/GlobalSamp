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
                    ? "Ваш логин: {18ff00}зарегистрирован на сервере.\n Для входа введите пароль."
                    : "Ваш логин: {ff0000}не зарегистрирован сервере.\n Для регистрации введите пароль.",
                true, "Далее")
        {
        }

        public void OnInputRegistrationData(object sender, DialogResponseEventArgs e)
        {
            var setpwd = new InputDialog("Повтор пароля", "Введите ваш пароль ещё раз:", true, "Далее");
            var setemail = new InputDialog("E-Mail", "Введите адрес Вашей электронной почты. /n" +
                                                     "{18ff00} С помощью почты вы можете восстановить доступ /n" +
                                                     "{18ff00} к вашему аккаунту в случае взлома или потери пароля./n" +
                                                     "На вашу почту мы отправим письмо со ссылкой. /n" +
                                                     "Перейдите по ней для подтверждения регистрации.", false, "Далее");
            var gender = new InputDialog("Пол", "Выберите Ваш пол.", false, "Мужской", "Женский");
            var skincolor = new InputDialog("Цвет кожи", "Выберите Ваш цвет кожи.", false, "Светлый", "Тёмный");
            // var Nationality = new InputDialog("Национальность", "Выберите Вашу национальность", false, "Далее");
            var nationalitylist = new TablistDialog("Национальность", 15, "Далее", "В начало");
            nationalitylist.Add(new[]
            {
                "Албанец"
            });
            var age = new InputDialog("Возраст", "Введите Ваш возраст", false, "Далее");
            // var Promo1 = new InputDialog("Откуда вы узнали о GlobalRP?", "От друзей /n" +
            //                                                             "На YouTube /n" +
            //                                                             "Вкладка Hosted /n" +
            //                                                             "На порталах/форумах /n" +
            //                                                             "В поисковике /n" +
            //                                                             "Другое", false, "Далее");
            var friend = new InputDialog("Введите ник друга, который тут играет",
                "{18ff00} После получения Вами пятого уровня он получит вознаграждение", false, "Вввести", "Далее");

            var promo = new InputDialog("Промокод", "Введите промокод. /n" +
                                                    "С помощью промокода {18ff00}Вы получите преимущество на старте игры",
                false, "Ввести", "Пропустить");
            if (e.DialogButton != DialogButton.Left)
            {
                Response -= OnInputRegistrationData;
                BasePlayer player = BasePlayer.Find(e.Player.Id);
                if (player == null)
                {
                    return;
                }

                player.SendClientMessage("...");
                player.Kick();
                return;
            }

            PlayerData data = PlayerManager.Instance.GetPlayerData(e.Player.Name);
            if (data.Password == e.InputText)
            {
                BasePlayer player = BasePlayer.Find(e.Player.Id);
                data.Authorized = true;
                setpwd.Show(player);
            }
            else
            {

            }

            Response -= OnInputRegistrationData;

        }
    }
}