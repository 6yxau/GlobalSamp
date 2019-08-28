using System.Collections.Generic;
using GlobalSamp.Player.Dao;
using GlobalSamp.Tools.Common;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace GlobalSamp.Player
{
    public sealed class PlayerManager : Singleton<PlayerManager>
    {
        private readonly PlayerDao _dao;
        
        private readonly Dictionary<string, PlayerData> _players = new Dictionary<string, PlayerData>(1000);
        public PlayerManager()
        {
            _dao = new PlayerDao();
        }

        public PlayerData GetPlayerData(string name)
        {
            if (_players.ContainsKey(name))
            {
                return _players[name];
            }
            PlayerData data = _dao.GetPlayerModel(name);
            if (data == null)
            {
                return null;
            }
            _players.Add(name, data);
            return data;
        }

        public bool RemovePlayerData(string name)
        {
            return _players.Remove(name);
        }
        
    }
}