using GlobalSamp.Player.Dao;
using GlobalSamp.Tools.Common;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace GlobalSamp.Player
{
    public sealed class PlayerManager : Singleton<PlayerManager>
    {
        private readonly PlayerDao _dao;

        public PlayerManager()
        {
            _dao = new PlayerDao();
        }

        public InputDialog GetConnectionDialogForPlayer(BasePlayer player)
        {
            string caption = null;
            string message = null;
            string b1 = null;
            if (!_dao.IsUserRegistered(player.Name))
            {
                caption = "Регистрация";
                message = "Пожалуйста, зарегистрируйтесь!";
                b1 = "Региситрация";
            }
            else
            {
                caption = "Вход";
                message = "Введите ваш пароль. Если Вы ещё не играли на нашем сервере, то смените ник.";
                b1 = "Вход";
            } 
            return new InputDialog(caption, message, true, b1, $"Выход");
        }
    }
}