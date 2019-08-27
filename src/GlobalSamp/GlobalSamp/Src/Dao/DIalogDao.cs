using System;
using SampSharp.GameMode.Display;

namespace GlobalSamp.Dao
{
    public class DIalogDao
    {
        protected void OnRegisterOpen()
        {

            var SetPwd = new InputDialog("Повтор пароля", "Введите ваш пароль ещё раз:", true, "Далее");
            var SetEmail = new InputDialog("E-Mail", "Введите адрес Вашей электронной почты. /n" +
                                                     "{18ff00} С помощью почты вы можете восстановить доступ /n" +
                                                     "{18ff00} к вашему аккаунту в случае взлома или потери пароля./n" +
                                                     "На вашу почту мы отправим письмо со ссылкой. /n" +
                                                     "Перейдите по ней для подтверждения регистрации.", false, "Далее");
            var Gender = new InputDialog("Пол", "Выберите Ваш пол.", false, "Мужской", "Женский");
            var SkinColor = new InputDialog("Цвет кожи", "Выберите Ваш цвет кожи.", false, "Светлый", "Тёмный");
            // var Nationality = new InputDialog("Национальность", "Выберите Вашу национальность", false, "Далее");
            var NationalityList = new TablistDialog("Национальность", 15, "Далее", "В начало");
            NationalityList.Add(new[]
            {
                "Албанец"
            });
            var Age = new InputDialog("Возраст", "Введите Ваш возраст", false, "Далее");
            var Promo1 = new InputDialog("Откуда вы узнали о GlobalRP?", "От друзей /n" +
                                                                         "На YouTube /n" +
                                                                         "Вкладка Hosted /n" +
                                                                         "На порталах/форумах /n" +
                                                                         "В поисковике /n" +
                                                                         "Другое", false, "Далее");
            var Friend = new InputDialog("Введите ник друга, который тут играет", "{18ff00} После получения Вами пятого уровня он получит вознаграждение", false, "Вввести", "Далее");
            var Promo2 = new InputDialog("Промокод", "Введите промокод. /n" +
                                                     "С помощью промокода {18ff00}Вы получите преимущество на старте игры", false, "Ввести", "Пропустить");
        }
    }
}